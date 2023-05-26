using Models;
using System.Collections.Generic;

namespace Interfaces.Repository
{
    public interface IPharmacyRepository
    {
        List<Medicine> GetAllMedicine();
        void AddMedicine(Medicine medicine);
        void UpdateMedicine(Medicine medicine);
        void DeleteMedicine(Medicine medicine);

        List<Pharmacy> GetAllPharmacies();
        void AddPharmacy(Pharmacy pharmacy);
        void UpdatePharmacy(Pharmacy pharmacy);
        void DeletePharmacy(Pharmacy pharmacy);


        List<PharmacyMedicine> GetAllMedicineForPharmacy(int pharmacyId);
        void UpdateMedicineInPharmacy(PharmacyMedicine medicine);

    }
}
