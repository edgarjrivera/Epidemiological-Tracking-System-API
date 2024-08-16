using EpidemiologicalTrackingApi.Models;

namespace EpidemiologicalTrackingApi.Contracts
{
    public interface IIndividualService
    {
        Individual FindIndividualById(int id);
        void UpdateIndividualFields(Individual individual, Individual updatedIndividual);
    }
}
