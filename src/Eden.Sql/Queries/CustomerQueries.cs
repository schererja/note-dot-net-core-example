using System.Diagnostics.CodeAnalysis;

namespace Eden.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class CustomerQueries
    {
        public static string AllCustomers => "Select * from Customers";
        public static string GetCustomerByCustomerNumber => "exec [dc_spGetCustomerByID] @CustNum";
    }
}
