using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Service
{
    public interface IPharmacyService
    {
        List<Medicine> GetAllMedicine(int currentUserID);
        void AddMedicine(Medicine medicine, int currentUserId);
        void UpdateMedicine(Medicine medicine, int currentUserId);
        void DeleteMedicine(Medicine medicine, int currentUserId);

        List<Pharmacy> GetAllPharmacies(int currentUserID);
        void AddPharmacy(Pharmacy pharmacy, int currentUserId);
        void UpdatePharmacy(Pharmacy pharmacy, int currentUserId);
        void DeletePharmacy(Pharmacy pharmacy, int currentUserId);


        List<PharmacyMedicine> GetAllMedicineForPharmacy(int pharmacyId, int currentUserId);
        void UpdateMedicineInPharmacy(PharmacyMedicine medicine, int currentUserId);
    }
}
