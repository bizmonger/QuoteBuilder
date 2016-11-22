namespace Transaction
{
    public partial class Registry
    {
        public delegate void RegistryClearedHandler();
        public event RegistryClearedHandler RegistryCleared;
    }
}