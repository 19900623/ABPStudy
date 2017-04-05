using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.RealTime
{
    /// <summary>
    /// Implements <see cref="IOnlineClientManager"/>.
    /// <see cref="IOnlineClientManager"/>��ʵ��
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        /// <summary>
        /// �ͻ��������¼�
        /// </summary>
        public event EventHandler<OnlineClientEventArgs> ClientConnected;

        /// <summary>
        /// �ͻ��˶Ͽ��¼�
        /// </summary>
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;

        /// <summary>
        /// �û������¼�
        /// </summary>
        public event EventHandler<OnlineUserEventArgs> UserConnected;

        /// <summary>
        /// �û��Ͽ��¼�
        /// </summary>
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// Online clients.
        /// �ͻ����ֵ伯��
        /// </summary>
        private readonly ConcurrentDictionary<string, IOnlineClient> _clients;

        private readonly object _syncObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClientManager"/> class.
        /// ��ʼ��<see cref="OnlineClientManager"/>���µ�ʵ��
        /// </summary>
        public OnlineClientManager()
        {
            _clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        /// <summary>
        /// ���һ���ͻ���
        /// </summary>
        /// <param name="client">�ͻ���</param>
        public void Add(IOnlineClient client)
        {
            lock (_syncObj)
            {
                var userWasAlreadyOnline = false;
                var user = client.ToUserIdentifierOrNull();

                if (user != null)
                {
                    userWasAlreadyOnline = this.IsOnline(user);
                }

                _clients[client.ConnectionId] = client;

                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (user != null && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                }
            }
        }

        /// <summary>
        /// ͨ������ID�Ƴ�һ���ͻ���
        /// </summary>
        /// <param name="connectionId">����ID</param>
        /// <returns>����Ƴ��򷵻�True</returns>
        public bool Remove(string connectionId)
        {
            lock (_syncObj)
            {
                IOnlineClient client;
                var isRemoved = _clients.TryRemove(connectionId, out client);

                if (isRemoved)
                {
                    var user = client.ToUserIdentifierOrNull();

                    if (user != null && !this.IsOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }

                    ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                }

                return isRemoved;
            }
        }

        /// <summary>
        /// ͨ������ID���ҿͻ��ˣ����û�ҵ��򷵻�null
        /// </summary>
        /// <param name="connectionId">����ID</param>
        /// <returns></returns>
        public IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            lock (_syncObj)
            {
                return _clients.GetOrDefault(connectionId);
            }
        }

        /// <summary>
        /// ��ȡ�������߿ͻ���
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllClients()
        {
            lock (_syncObj)
            {
                return _clients.Values.ToImmutableList();
            }
        }
    }
}