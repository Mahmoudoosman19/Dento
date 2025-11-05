namespace Common.Application.Extensions.Mapster
{
    public class UpdateNestedListDto<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
