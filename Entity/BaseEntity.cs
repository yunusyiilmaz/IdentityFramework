using Microsoft.EntityFrameworkCore;

namespace Core.Concrete
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatDate { get; set; }
    }
}
