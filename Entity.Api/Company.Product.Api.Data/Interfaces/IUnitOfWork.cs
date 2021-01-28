namespace Company.Product.Data.Interfaces
{
	public interface IUnitOfWork
	{
		bool CanConnectToDatabase();
	}
}
