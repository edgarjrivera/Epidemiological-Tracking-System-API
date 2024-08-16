using EpidemiologicalTrackingApi.Contracts;
using EpidemiologicalTrackingApi.Data;
using EpidemiologicalTrackingApi.Exceptions;
using EpidemiologicalTrackingApi.Models;

namespace EpidemiologicalTrackingApi.Services
{
    public class IndividualService : IIndividualService
    {
        public Individual FindIndividualById(int id)
        {
            var individual = DataStore.Individuals.FirstOrDefault(i => i.Id == id);
            if (individual == null)
            {
                throw new IndividualNotFoundException(id);
            }
            return individual;
        }

        public void UpdateIndividualFields(Individual individual, Individual updatedIndividual)
        {
            individual.Name = updatedIndividual.Name;
            individual.Age = updatedIndividual.Age;
            individual.Symptoms = updatedIndividual.Symptoms;
            individual.Diagnosed = updatedIndividual.Diagnosed;
            individual.DateDiagnosed = updatedIndividual.DateDiagnosed;
        }
    }
}
