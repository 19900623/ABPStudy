namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to return a page of items to clients.
    /// �˽ӿڶ��巵�ؿͻ��˽������һҳ
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items"/> list / <see cref="IListResult{T}.Items"/>���������</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}