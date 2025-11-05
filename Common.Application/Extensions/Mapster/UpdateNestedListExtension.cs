using Common.Domain.Primitives;
using MapsterMapper;

namespace Common.Application.Extensions.Mapster
{
    public static class UpdateNestedListExtension
    {
        public static void UpdateNestedListObject<T, TEntity, TKey>(
            this List<T> src,
            List<TEntity> dest,
            IMapper mapper)
            where T : UpdateNestedListDto<TKey>
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            dest = src.Join(dest, x => x.Id, x => x.Id, (x, y) => mapper.Map(x, y)).ToList();
        }
    }
}
