namespace DataLayer
{
    public interface IGeneticsRepository
    {
        Animal Find(int animalId);
        List<Animal> GetAll();
        Animal Add(Animal animal);
        Animal Update(Animal animal);
        void Remove(int animalId);
    }
}