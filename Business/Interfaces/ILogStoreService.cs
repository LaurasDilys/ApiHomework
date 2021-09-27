using Business.Dto;

namespace Business.Interfaces
{
    public interface ILogStoreService
    {
        public void Create(LogRequest request);

        public bool LocationIsReadable();

        public LogResponseDtoArray All();

        public bool Exists(int key);

        public LogResponseDto Get(int key);
    }
}
