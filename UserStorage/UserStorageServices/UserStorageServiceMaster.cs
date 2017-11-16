using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private List<UserStorageServiceSlave> slaveServices;
        private List<INotificationSubscriber> subscribers = new List<INotificationSubscriber>();

        public UserStorageServiceMaster(IEnumerable<UserStorageServiceSlave> slaves, IUserValidate<User> validator = null)
            : base(validator)
        {
            if (slaves != null)
            {
                this.slaveServices = slaves.ToList();
            }
            else
            {
                this.slaveServices = new List<UserStorageServiceSlave>();
            }
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            base.Add(user);
            foreach (var ss in this.slaveServices)
            {
                ss.Add(user);
            }

            foreach (var sub in this.subscribers)
            {
                sub.UserAdded(user);
            }
        }

        public override void Remove(User user)
        {
            base.Remove(user);
            foreach (var ss in this.slaveServices)
            {
                ss.Remove(user);
            }

            foreach (var sub in this.subscribers)
            {
                sub.UserRemoved(user);
            }
        }

        public override IEnumerable<User> Search(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"Argument {nameof(predicate)} is null");
            }

            List<User> result = new List<User>();
            foreach (var service in this.slaveServices)
            {
                if (service.Search(predicate) != null)
                {
                    result.AddRange(service.Search(predicate));
                }
            }

            return result;
        }

        public void AddSubscriber(INotificationSubscriber sub)
        {
            if (sub == null)
            {
                throw new ArgumentNullException($"{nameof(sub)} is null");
            }

            this.subscribers.Add(sub);
        }

        public void RemoveSubscriber(INotificationSubscriber sub)
        {
            if (sub == null)
            {
                throw new ArgumentNullException($"{nameof(sub)} is null");
            }

            if (!this.subscribers.Contains(sub))
            {
                throw new InvalidOperationException("No such subscruber was found");
            }

            this.subscribers.Remove(sub);
        }
    }
}