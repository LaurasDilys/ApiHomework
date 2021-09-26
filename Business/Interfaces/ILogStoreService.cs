using Business.Dto;

namespace Business.Interfaces
{
    public interface ILogStoreService
    {
        public void Create(LogDtoArray request);

        public bool LocationIsReadable();

        public LogDtoArray All();

        public bool Exists(int key);

        public LogDto Get(int key);
    }
}
