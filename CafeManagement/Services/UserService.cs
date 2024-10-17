using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(User item)
        {
            _unitOfWork.User.Add(item);
        }

        public void Delete(User item)
        {
            if (item != null)
                _unitOfWork.User.Delete(item);
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.User.GetAll();
        }

        public User GetById(Guid id)
        {
            return _unitOfWork.User.GetById(id);
        }

        public void Update(User item)
        {
            if (item != null)
                _unitOfWork.User.Update(item);
        }
    }
}
