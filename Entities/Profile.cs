﻿using SQLite;

namespace Entities
{
    public class Profile : EntityBase
    {
        string _id = null;
        [PrimaryKey]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value?.Trim())
                {
                    _id = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        string _firstName = null;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value?.Trim())
                {
                    _firstName = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _lastName = null;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value?.Trim())
                {
                    _lastName = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _businessName = null;
        public string BusinessName
        {
            get { return _businessName; }
            set
            {
                if (_businessName != value?.Trim())
                {
                    _businessName = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _phone = null;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value?.Trim())
                {
                    _phone = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _email = null;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value?.Trim())
                {
                    _email = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _postal = null;
        public string Postal
        {
            get { return _postal; }
            set
            {
                if (_postal != value?.Trim())
                {
                    _postal = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _city = null;
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value?.Trim())
                {
                    _city = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _state = null;
        public string State
        {
            get { return _state; }
            set
            {
                if (_state != value?.Trim())
                {
                    _state = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _address1 = null;
        public string Address1
        {
            get { return _address1; }
            set
            {
                if (_address1 != value?.Trim())
                {
                    _address1 = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _address2 = null;
        public string Address2
        {
            get { return _address2; }
            set
            {
                if (_address2 != value?.Trim())
                {
                    _address2 = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
    }
}