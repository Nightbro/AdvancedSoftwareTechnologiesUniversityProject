using ViewModels.ViewModels;
using System.Collections.Generic;

namespace Interfaces.Map
{
    public interface IPharmacyMap
    {

        List<MedicineViewModel> GetAllMedicine(int currentUserID);
        void AddMedicine(MedicineViewModel medicine, int currentUserId);
        void UpdateMedicine(MedicineViewModel medicine, int currentUserId);
        void DeleteMedicine(MedicineViewModel medicine, int currentUserId);

        List<PharmacyViewModel> GetAllPharmacies(int currentUserID);
        void AddPharmacy(PharmacyViewModel pharmacy, int currentUserId);
        void UpdatePharmacy(PharmacyViewModel pharmacy, int currentUserId);
        void DeletePharmacy(PharmacyViewModel pharmacy, int currentUserId);


        List<PharmacyMedicineViewModel> GetAllMedicineForPharmacy(int pharmacyId, int currentUserId);
        void UpdateMedicineInPharmacy(PharmacyMedicineViewModel medicine, int currentUserId);

    }
}
