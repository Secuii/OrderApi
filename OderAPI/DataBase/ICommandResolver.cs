namespace OrderAPI.DataBase
{
    public interface ICommandResolver
    {
        public Response Resolve<Request, Response>(Request request) where Request : DataBase.Request where Response : DataBase.Response;

    }
}
