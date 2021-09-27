using Business.Dto;

namespace Business.Interfaces
{
    public interface IReadableLogLocation
    {
        public LogResponseDtoArray All();
        public bool Exists(int key);
        public LogResponseDto Get(int key);
    }
}
