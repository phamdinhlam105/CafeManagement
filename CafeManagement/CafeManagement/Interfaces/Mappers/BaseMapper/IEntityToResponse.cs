namespace CafeManagement.Interfaces.Mappers.BaseMapper
{
    public interface IEntityToResponse<TEntity, TRes> 
        where TEntity : class 
        where TRes : class
    {
        TRes MapToResponse(TEntity entity);
    }
}
