

namespace Model
{
    public interface IDomainObject<Tkey>
    {
        Tkey ID { get; }
    }
}
