using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Entities;
using IO;
using OperationDependencies;

namespace Documents.Generation
{
    public partial class DocumentGenerator : IDocumentGenerator
    {
        public string ExecuteAsync(ViewQuoteDependencies dependencies)
        {
            var document = new Document()
            {
                Statement = dependencies.Quote,
                Logo = dependencies.Logo,
                Customer = dependencies.Customer
            };

            return ExecuteAsync(dependencies.FileReader, document);
        }

        public string ExecuteAsync(IRead fileReader, Document document)
        {
            Debug.Assert(document != null);
            Debug.Assert(document.Statement != null);

            var logo64Base = fileReader.GetImagebase64(document.Logo);

            var quoteTemplate = fileReader.Read(TEMPLATE);
            var serviceTemplate = fileReader.Read(SERVICE_TEMPLATE);
            var materialsTemplate = fileReader.Read(MATERIAL_TEMPLATE);

            var updatedTemplate1 = ApplyHeaderAsync(quoteTemplate, document.Statement, document.Customer, logo64Base);
            var updatedTemplate2 = ApplySummaryAsync(updatedTemplate1, document.Statement);

            var statement = document.Statement;
            var servicesHtml = ApplyServices(statement.Services, serviceTemplate);

            var html = updatedTemplate2;
            var htmlWithServices = html.Replace(serviceTemplate, servicesHtml);

            var materialsBuilder = new StringBuilder();
            var services = statement.Services;

            string htmlWithMaterial = null;
            bool hasMaterials = false;

            foreach (var service in services)
            {
                string materialsHtml = string.Empty;

                if (service.Materials == null)
                {
                    service.Materials = new ObservableCollection<Material>();
                }

                if (service.Materials.Count > 0)
                {
                    hasMaterials = true;

                    string materialsTable = ApplyMaterials(service, materialsTemplate);
                    materialsBuilder.Append(materialsTable);

                    materialsHtml = materialsBuilder.ToString();

                    htmlWithMaterial = htmlWithServices.Replace(materialsTemplate, materialsHtml).Replace("~SERVICE_NAME~", string.Empty);
                }
            }

            if (hasMaterials)
            {
                var destinationFile = string.Format("{0}{1}", Guid.NewGuid(), HtmlFile);
                return fileReader.CreateFile(htmlWithMaterial, destinationFile);
            }
            else
            {
                var data = fileReader.Read(SERVICE_MATERIAL_TEMPLATE);
                bool exists = htmlWithServices.Contains(data);

                string noMaterialsHtml = htmlWithServices.Replace(data, string.Empty);
                var destinationFile = string.Format("{0}{1}", Guid.NewGuid(), HtmlFile);
                return fileReader.CreateFile(noMaterialsHtml, destinationFile);
            }
        }

        public string ApplyHeaderAsync(string quoteTemplate, Statement statement, Customer customer, string logo)
        {
            var headerTemplate = UpdateHeaderAsync(quoteTemplate, statement, customer, logo);
            return quoteTemplate.Replace(quoteTemplate, headerTemplate);
        }

        public string ApplySummaryAsync(string quoteTemplate, Statement statement)
        {
            var footerTemplate = UpdateSummaryAsync(quoteTemplate, statement);
            return quoteTemplate.Replace(quoteTemplate, footerTemplate);
        }

        public string ApplyServices(string quoteTemplate, List<Service> services, string serviceTemplate)
        {
            var serviceBlocks = new List<string>();
            AddRow(services, serviceBlocks, serviceTemplate);

            var servicesBuilder = new StringBuilder();
            UpdateTemplate(serviceBlocks, servicesBuilder);

            return quoteTemplate.Replace(serviceTemplate, servicesBuilder.ToString());
        }

        public string ApplyServices(List<Service> services, string serviceTemplate)
        {
            var serviceBlocks = new List<string>();
            AddRow(services, serviceBlocks, serviceTemplate);

            var servicesBuilder = new StringBuilder();
            UpdateTemplate(serviceBlocks, servicesBuilder);

            return servicesBuilder.ToString();
        }

        public string ApplyMaterials(string quoteTemplate, Service service, string materialTemplate)
        {
            var materialBlocks = new List<string>();

            AddRows(service.Materials, materialBlocks, materialTemplate);

            var materialsBuilder = new StringBuilder();
            UpdateTemplate(materialBlocks, materialsBuilder);

            materialsBuilder.Append("</table>");

            var partialTemplateUpdate = quoteTemplate.Replace(materialTemplate, materialsBuilder.ToString());
            var fullTemplateUpdate = partialTemplateUpdate.Replace("~SERVICE_NAME~", "Materials");

            return fullTemplateUpdate;
        }

        public string ApplyMaterials(Service service, string materialTemplate)
        {
            var materialBlocks = new List<string>();

            AddRows(service.Materials, materialBlocks, materialTemplate);

            var materialsBuilder = new StringBuilder();
            UpdateTemplate(materialBlocks, materialsBuilder);

            return materialsBuilder.ToString();
        }

        #region Helpers
        void UpdateTemplate(List<string> blocks, StringBuilder stringBuilder)
        {
            foreach (var block in blocks)
            {
                stringBuilder.Append(block);
                stringBuilder.Append("\n");
            }
        }

        void AddRow(List<Service> services, List<string> serviceBlocks, string serviceTemplate)
        {
            foreach (var service in services)
            {
                var serviceBlock = UpdateBlock(serviceTemplate, service);
                serviceBlocks.Add(serviceBlock);
            }
        }

        void AddRows(ObservableCollection<Material> materials, List<string> materialBlocks, string materialTemplate)
        {
            foreach (var material in materials)
            {
                var materialBlock = UpdateBlock(materialTemplate, material);
                materialBlocks.Add(materialBlock);
            }
        }

        string UpdateHeaderAsync(string headerTemplate, Statement statement, Customer customer, string logo)
        {
            var headerBlock = new StringBuilder(headerTemplate);

            var profile = statement.Profile;
            var address = statement.Address;

            headerBlock = headerBlock.Replace("~HEADER_LOGO~", logo);

            headerBlock = headerBlock.Replace("~TITLE~", statement.TypeName);
            headerBlock = headerBlock.Replace("~PROVIDE_COMPANY_NAME~", profile.BusinessName);
            headerBlock = headerBlock.Replace("~PROVIDER_STREET_ADDRESS1~", profile.Address1);
            headerBlock = headerBlock.Replace("~PROVIDER_STREET_ADDRESS2~", profile.Address2);
            headerBlock = headerBlock.Replace("~PROVIDER_CITY_STATE_ZIP~", profile.Postal);
            headerBlock = headerBlock.Replace("~PROVIDER_PHONE~", profile.Phone);
            headerBlock = headerBlock.Replace("~PROVIDER_EMAIL~", profile.Email);
            headerBlock = headerBlock.Replace("Comments", statement.Title);
            ProcessAppDifferences(statement, headerBlock);
            headerBlock = headerBlock.Replace("~STATEMENT_NUMBER~", statement.StatementNumber.ToString());
            headerBlock = headerBlock.Replace("~STATEMENT_FOR~", string.Format("{0} for", statement.TypeName));

            try
            {
                //SQLiteAsyncConnection connection = new SQLiteAsyncConnection(Bizmonger.Client.Infrastructure.Constants.DATABASE_FILE_NAME);

                //var customerId = statement.CustomerId.ToString();
                //var customerQuery = connection.Table<Bizmonger.Client.Infrastructure.DAL.Entities.Customer>().
                //    Where(c => c.CustomerId == customerId);

                //var customer = await customerQuery.FirstOrDefaultAsync();

                headerBlock = headerBlock.Replace("~CLIENT_NAME~", string.Format("{0} {1}", customer.FirstName, customer.LastName));
                headerBlock = headerBlock.Replace("~CLIENT_EMAIL~", customer.Email);
                headerBlock = headerBlock.Replace("~CLIENT_PHONE~", customer.Phone);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to query customer data:{ex.Message}");
            }

            return headerBlock.ToString();
        }

        void ProcessAppDifferences(Statement statement, StringBuilder textblock)
        {
            if (statement.TypeName == Constants.STATEMENT_TYPE_QUOTE)
            {
                textblock = textblock.Replace("~DATE_LABEL~", string.Empty);
                textblock = textblock.Replace("~DATE_VALUE~", DateTime.Now.ToString("d", DateTimeFormatInfo.InvariantInfo));

                textblock = textblock.Replace("~SLOGAN~", @"Provided By: <br/>Quote Builder");
            }
            else if (statement.TypeName == Constants.STATEMENT_TYPE_INVOICE)
            {
                textblock = textblock.Replace("~DATE_LABEL~", "Due Date:");
                textblock = textblock.Replace("~DATE_VALUE~", (statement as Invoice).DueDate.ToString("d", DateTimeFormatInfo.InvariantInfo));

                textblock = textblock.Replace("~SLOGAN~", @"Provided By: <br/>Quote Viewer");
            }
            else
            {
                Debug.Assert(false, "Unexpected statement type");
            }
        }

        void ProcessAmount(Statement statement, StringBuilder textblock)
        {
            if (statement.TypeName == Constants.STATEMENT_TYPE_QUOTE)
            {
                textblock = textblock.Replace("~TOTAL_COST_LABEL~", "Total Cost:");
            }
            else if (statement.TypeName == Constants.STATEMENT_TYPE_INVOICE)
            {
                textblock = textblock.Replace("~TOTAL_COST_LABEL~", "Amount Due:");
            }
            else
            {
                Debug.Assert(false, "Unexpected statement type");
            }

            textblock = textblock.Replace("~TOTAL_COST~", statement.Total.ToString());
        }

        string UpdateSummaryAsync(string footerTemplate, Statement statement)
        {
            var footerBlock = new StringBuilder(footerTemplate);

            footerBlock = footerBlock.Replace("~STATEMENT_NUMBER~", statement.StatementNumber.ToString());
            ProcessAppDifferences(statement, footerBlock);
            ProcessAmount(statement, footerBlock);
            footerBlock = footerBlock.Replace("~THANK_YOU_MESSAGE~", "Thank you for your interest!");

            return footerBlock.ToString();
        }

        string UpdateBlock(string serviceTemplate, Service service)
        {
            var serviceBlock = new StringBuilder(serviceTemplate);

            serviceBlock = serviceBlock.Replace("~SERVICE_NAME~", service.Name);
            serviceBlock = serviceBlock.Replace("~SERVICE_DESCRIPTION~", service.Description);
            serviceBlock = serviceBlock.Replace("~SERVICE_AMOUNT~", service.LaborCost.ToString());
            serviceBlock = serviceBlock.Replace("~SERVICE_TAX~", string.Format("{0}%", service.TaxPercentage.ToString()));

            return serviceBlock.ToString();
        }

        string UpdateBlock(string materialTemplate, Material material)
        {
            var materialBlock = new StringBuilder(materialTemplate);

            materialBlock = materialBlock.Replace("~MATERIAL_NAME~", material.Name);
            materialBlock = materialBlock.Replace("~MATERIAL_DESCRIPTION~", material.Description);
            materialBlock = materialBlock.Replace("~MATERIAL_QUANTITY~", string.Format("{0} {1}", material.Quantity, material.UnitType));
            materialBlock = materialBlock.Replace("~MATERIAL_COST~", (material.MarkupPrice * material.Quantity).ToString());

            return materialBlock.ToString();
        }
        #endregion
    }
}
