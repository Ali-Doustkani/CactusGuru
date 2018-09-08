namespace CactusGuru.Infrastructure.Persistance.Merging
{
    public class Merger<T> : MergerAlgorithm<T>
         where T : DomainEntity
    {
        public Merger(Publisher<T> publisher, Terminator<T> terminator)
        {
            _publisher = publisher;
            _terminator = terminator;
        }

        private readonly Publisher<T> _publisher;
        private readonly Terminator<T> _terminator;

        protected override void AddImp(T item)
        {
            _publisher.Add(item);
        }

        protected override void UpdateImp(T item)
        {
            _publisher.Update(item);
        }

        protected override void DeleteImp(T item)
        {
            _terminator.Terminate(item.Id);
        }
    }
}
