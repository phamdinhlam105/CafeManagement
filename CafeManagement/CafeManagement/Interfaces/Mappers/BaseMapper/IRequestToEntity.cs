namespace CafeManagement.Interfaces.Mappers.BaseMapper
{
    public interface IRequestToEntity<TReq, TEntity> 
        where TReq : class 
        where TEntity : class
    {
        TEntity MapToEntity(TReq req);
    }
}
