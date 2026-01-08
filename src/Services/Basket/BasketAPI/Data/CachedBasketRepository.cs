
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BasketAPI.Data
{
    public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            await repo.DeleteBasket(username, cancellationToken);

            await cache.RemoveAsync(username, cancellationToken);

            return true;

        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            //1. Cache check
            var cachedBasket = await cache.GetStringAsync(username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                //1a. Cache hit - retrieve from cache
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            //2. Cache miss - retrieve from db
            var basket = await GetBasket(username, cancellationToken);

            //3. Refresh cache
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repo.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;

        }
    }
}

