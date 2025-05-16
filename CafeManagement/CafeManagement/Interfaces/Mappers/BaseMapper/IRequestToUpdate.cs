namespace CafeManagement.Interfaces.Mappers.BaseMapper
{
    public interface IRequestToUpdate<TReq, TEntity> 
        where TReq : class 
        where TEntity : class
    {
        void UpdateEntityFromRequest(TReq req, TEntity entity);
    }
}
