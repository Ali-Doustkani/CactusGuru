namespace CactusGuru.Infrastructure.Persistance.Merging
{
    public class Merger<T> : MergerAlgorithm<T>
         where T : DomainEntity
    {
        public Merger(IPublisher<T> publisher, ITerminator<T> terminator)
        {
            _publisher = publisher;
            _terminator = terminator;
        }

        private readonly IPublisher<T> _publisher;
        private readonly ITerminator<T> _terminator;

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
