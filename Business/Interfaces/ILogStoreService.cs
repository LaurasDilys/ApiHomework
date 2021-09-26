using Business.Dto;

namespace Business.Interfaces
{
    public interface ILogStoreService
    {
        public void Create(LogRequest request);

        public bool LocationIsReadable();

        public LogRequest All();

        public bool Exists(int key);

        public LogDto Get(int key);
    }
}
