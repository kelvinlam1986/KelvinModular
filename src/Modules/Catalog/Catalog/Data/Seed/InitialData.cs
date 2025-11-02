namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(new Guid("CE9D771E-FF13-43BD-B286-3687A28A1560"), "IPhone X",
                    new List<string> { "mobile" }, "IPhone X", "", 500),
                Product.Create(new Guid("678E09FC-A1C2-43AB-9824-BC79B15B7D63"), "Sam Sung",
                    new List<string> { "mobile" }, "Sam Sung", "", 400),
                Product.Create(new Guid("8BD0C264-5EA1-4FEB-A5B6-BBBB14E9DEAF"), "Poco",
                    new List<string> { "mobile" }, "Pocp", "", 300),
            };
    }
}
