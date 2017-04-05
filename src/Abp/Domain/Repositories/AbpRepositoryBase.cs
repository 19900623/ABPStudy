using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;

namespace Abp.Domain.Repositories
{
    /// <summary>
    /// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.It implements some methods in most simple way.
    /// ʵ�ֽӿ�<see cref="IRepository{TEntity,TPrimaryKey}"/>�Ļ��ࡣ������򵥵ķ�ʽʵ����һЩ������
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository / �ִ�����ʵ�������</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity / ʵ����������</typeparam>
    public abstract class AbpRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region ����
        /// <summary>
        /// The multi tenancy side / �⻧˫��
        /// </summary>
        public static MultiTenancySides? MultiTenancySide { get; private set; }

        /// <summary>
        /// ����ע����������ȡ��
        /// </summary>
        public IIocResolver IocResolver { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        static AbpRepositoryBase()
        {
            var attr = typeof (TEntity).GetSingleAttributeOfTypeOrBaseTypesOrNull<MultiTenancySideAttribute>();
            if (attr != null)
            {
                MultiTenancySide = attr.Side;
            }
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// ��ȡһ��IQueryalbe�������Դ����ű��л�ȡʵ��
        /// Ҫʹ�ô˷�������ʹ��<see cref="UnitOfWorkAttribute"/> ���ԣ���Ϊ�˲�����Ҫʹ�ù�����Ԫ�������ݿ�����
        /// </summary>
        /// <returns>���ڴ����ݿ��л�ȡʵ���IQueryable</returns>
        public abstract IQueryable<TEntity> GetAll();

        /// <summary>
        /// ��ȡһ��IQueryalbe�������Դ����ű��л�ȡʵ��
        /// </summary>
        /// <param name="propertySelectors">���ʽ����</param>
        /// <returns>���ڴ����ݿ��л�ȡʵ���IQueryable</returns>
        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns>ʵ���б�</returns>
        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// �첽��ȡ����ʵ��
        /// </summary>
        /// <returns>ʵ���б�</returns>
        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        /// <summary>
        /// ���ڸ������ʽ <see cref="predicate"/> ����ȡ����ʵ��
        /// </summary>
        /// <param name="predicate">��ѯʵ�������</param>
        /// <returns>ʵ���б�</returns>
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        /// <summary>
        /// ���ڸ������ʽ <see cref="predicate"/> ����ȡ����ʵ�� - �첽
        /// </summary>
        /// <param name="predicate">��ѯʵ�������</param>
        /// <returns>ʵ���б�</returns>
        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        /// <summary>
        /// ����������ʵ�弯����ִ��һ����ѯ,�˲�����һ����Ҫ����<see cref="UnitOfWorkAttribute"/> (��<see cref="GetAll"/>�����෴)
        /// ���� <paramref name="queryMethod"/> ʹ�� ToList, FirstOrDefault �ȵ�
        /// </summary>
        /// <typeparam name="T">���ص�����</typeparam>
        /// <param name="queryMethod">��ѯʵ��ķ���</param>
        /// <returns>��ѯ���</returns>
        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        /// <summary>
        /// ͨ��������ȡʵ��
        /// </summary>
        /// <param name="id">����������</param>
        /// <returns>ʵ��</returns>
        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        /// <summary>
        /// ͨ��������ȡʵ�� - �첽
        /// </summary>
        /// <param name="id">����������</param>
        /// <returns>ʵ��</returns>
        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        /// <summary>
        /// ͨ�����ʽ��ȡһ��ȷ�е�ʵ�壬���ʵ�岻���ڻ��߶��ʵ�����׳��쳣
        /// </summary>
        /// <param name="predicate">���ʽ</param>
        /// <returns>ʵ��</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        /// <summary>
        /// ͨ�����ʽ��ȡһ��ȷ�е�ʵ�壬���ʵ�岻���ڻ��߶��ʵ�����׳��쳣 - �첽
        /// </summary>
        /// <param name="predicate">���ʽ</param>
        /// <returns>ʵ��</returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        /// <summary>
        /// ͨ��������ȡʵ�壬���û���ҵ��򷵻�Null
        /// </summary>
        /// <param name="id">ʵ������</param>
        /// <returns>ʵ�����Null</returns>
        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// ͨ��������ȡʵ�壬���û���ҵ��򷵻�Null - �첽
        /// </summary>
        /// <param name="id">ʵ������</param>
        /// <returns>ʵ�����Null</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        /// <summary>
        /// ͨ�������ı��ʽ��ȡ����ʵ�壬���û�ҵ��򷵻�Null
        /// </summary>
        /// <param name="predicate">��ѯʵ��ı��ʽ����</param>
        /// <returns>ʵ��</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        /// <summary>
        /// ͨ�������ı��ʽ��ȡ����ʵ�壬���û�ҵ��򷵻�Null - �첽
        /// </summary>
        /// <param name="predicate">��ѯʵ��ı��ʽ����</param>
        /// <returns>ʵ��</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        /// <summary>
        /// ͨ��������������ȡ����ʵ�壬���÷������ݿ�
        /// </summary>
        /// <param name="id">ʵ������</param>
        /// <returns>ʵ��</returns>
        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ���µ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public abstract TEntity Insert(TEntity entity);

        /// <summary>
        /// ����һ���µ�ʵ�� - �첽
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        /// <summary>
        /// ����ʵ���ȡ����ID����������Ҫ���浱ǰ�Ĺ�����Ԫ�Ա�õ�ID
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ���ID</returns>
        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        /// <summary>
        /// ����ʵ���ȡ����ID����������Ҫ���浱ǰ�Ĺ�����Ԫ�Ա�õ�ID - �첽
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ���ID</returns>
        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        /// <summary>
        /// ����ʵ���IDֵ��������߸���ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        /// <summary>
        /// ����ʵ���IDֵ��������߸���ʵ�� -  �첽
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        /// <summary>
        /// ����ʵ���IDֵ��������߸���ʵ��,����ʵ���ID����������Ҫ���浱ǰ�Ĺ�����Ԫ�Ա�õ�ID
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��ID</returns>
        public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        /// <summary>
        /// ����ʵ���IDֵ��������߸���ʵ�� - �첽
        /// ����ʵ���ID����������Ҫ���浱ǰ�Ĺ�����Ԫ�Ա�õ�ID
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ���ID</returns>
        public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }
        #endregion

        #region �޸�
        /// <summary>
        /// �����Ѵ��ڵ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public abstract TEntity Update(TEntity entity);

        /// <summary>
        /// �����Ѵ��ڵ�ʵ�� - �첽
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// ����һ���Ѵ��ڵ�ʵ��
        /// </summary>
        /// <param name="id">ʵ���ID</param>
        /// <param name="updateAction">�����޸�ʵ��ֵ�ķ���</param>
        /// <returns>�����ĵ�ʵ��</returns>
        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        /// <summary>
        /// ����һ���Ѵ��ڵ�ʵ�� - �첽
        /// </summary>
        /// <param name="id">ʵ���ID</param>
        /// <param name="updateAction">�����޸�ʵ��ֵ�ķ���</param>
        /// <returns>�����ĵ�ʵ��</returns>
        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }
        #endregion

        #region ɾ��
        /// <summary>
        /// ɾ��һ��ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// ɾ��һ��ʵ�� - �첽
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��������ɾ��ʵ��
        /// </summary>
        /// <param name="id">ʵ�������</param>
        public abstract void Delete(TPrimaryKey id);

        /// <summary>
        /// ��������ɾ��ʵ�� - �첽
        /// </summary>
        /// <param name="id">ʵ������</param>
        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        /// <summary>
        /// ͨ�����ʽɾ��ʵ��
        /// ע�⣺��������������ʵ�嶼������ȡ��ɾ��,���ɾ����ʵ���������࣬����ܻ�����ϴ��������⡣
        /// </summary>
        /// <param name="predicate">����ʵ��ı��ʽ</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// ͨ�����ʽɾ��ʵ�� - �첽
        /// ע�⣺��������������ʵ�嶼������ȡ��ɾ��,���ɾ����ʵ���������࣬����ܻ�����ϴ��������⡣
        /// </summary>
        /// <param name="predicate">����ʵ��ı��ʽ</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
        }
        #endregion

        #region �ۺ�
        /// <summary>
        /// ��ȡ��ǰ�ִ�������ʵ�������
        /// </summary>
        /// <returns>ʵ������</returns>
        public virtual int Count()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// ��ȡ��ǰ�ִ�������ʵ������� - �첽
        /// </summary>
        /// <returns>ʵ������</returns>
        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        /// <summary>
        /// ��ȡ�˲ִ�������������� <paramref name="predicate"/>��ʵ������
        /// </summary>
        /// <param name="predicate">һ������ʵ��ķ���</param>
        /// <returns>ʵ������</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        /// <summary>
        /// ��ȡ�˲ִ�������������� <paramref name="predicate"/>��ʵ������ - �첽
        /// </summary>
        /// <param name="predicate">һ������ʵ��ķ���</param>
        /// <returns>ʵ������</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        /// <summary>
        /// ��ȡ�˲ִ�������ʵ������� (�������ֵ���ܻᳬ��<see cref="int.MaxValue"/>.��ʹ�ô˷���)
        /// </summary>
        /// <returns>ʵ������</returns>
        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        /// <summary>
        /// ��ȡ�˲ִ�������ʵ������� (�������ֵ���ܻᳬ��<see cref="int.MaxValue"/>.��ʹ�ô˷���)
        /// </summary>
        /// <returns>ʵ������</returns>
        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        /// <summary>
        /// ��ȡ�˲ִ�������������� <paramref name="predicate"/>��ʵ������
        /// (�������ֵ���ܻᳬ��<see cref="int.MaxValue"/>.��ʹ�ô˷���)
        /// </summary>
        /// <param name="predicate">һ������ʵ��ķ���</param>
        /// <returns>ʵ������</returns>
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        /// <summary>
        /// ��ȡ�˲ִ�������������� <paramref name="predicate"/>��ʵ������ - �첽
        /// (�������ֵ���ܻᳬ��<see cref="int.MaxValue"/>.��ʹ�ô˷���)
        /// </summary>
        /// <param name="predicate">һ������ʵ��ķ���</param>
        /// <returns></returns>
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }
        #endregion

        /// <summary>
        /// Ϊʵ��Id������ͬlambda���ʽ
        /// </summary>
        /// <param name="id">ʵ��ID</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}