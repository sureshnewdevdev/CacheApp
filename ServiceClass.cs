using DataServices;

namespace CacheApp
{
   class Account
    {

    }
    public class ServiceClass // dll
    {
        CrudService<Account> crudService = new CrudService<Account>();

        public int InsertEmployeeData()
        {
            crudService.InsertDataProp = new InsertData<Account>(AccoutInsert);

            return crudService.CallService(new Account());
        }

        private int AccoutInsert(Account data)
        {
            return 1;
        }

        private int DBInsert(string data)
        {
            // Redis Cache Data
            //
            //

            return 1000;
        }
    }
}
