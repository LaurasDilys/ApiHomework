using Business.Dto;

namespace Business.Interfaces
{
    public interface ILogStoreLocation
    {
        public void Create(LogDtoArray request);
    }
}
