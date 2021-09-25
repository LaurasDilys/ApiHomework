using Business.Dto;

namespace Business.Interfaces
{
    public interface IReadableLogLocation
    {
        public LogRequest All();

        // key yra string tipo,
        // nes šiuo momentu galvoju
        // paiešką daryti pagal LogDto.Timestamp
        public bool Exists(string key);

        // alternatyviai išsaugotam logui galima priskirti {int Id}
        // tuomet šiems metomas reiktų {int key}
        public LogDto Get(string key);
    }
}
