namespace AzureTableProxy
{
    public class ContactData
    {
        // ReSharper disable once InconsistentNaming
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }

        public string PartitionKey => "1";
        public string RowKey => ID.ToString();
    }
}
