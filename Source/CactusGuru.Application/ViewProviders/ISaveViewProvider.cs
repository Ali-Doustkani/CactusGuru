namespace CactusGuru.Application.ViewProviders
{
    public interface ISaveViewProvider
    {
        string InquiryForDelete(object domainObject);
        void Delete(object domainObject);
        object Copy(object domainObject);
        void CopyTo(object from, object to);
        string Validate(object domainObject);
        void Add(object domainObject);
        void Update(object domainObject);
    }
}
