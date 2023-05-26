using Interfaces.Repository;
using Interfaces.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
    public class PharmacyService : BaseService,IPharmacyService
    {
        private IPharmacyRepository repository;
        public PharmacyService(IPharmacyRepository rep, IUserRepository userRepository) : base(userRepository)
        {
            repository = rep;
        }

        private bool isOwner(int pharmacyId, int currentUserId)
        {
            var pharmacies = repository.GetAllPharmacies();
            var currentPharmacy = pharmacies.Where(x =>x.Id == pharmacyId).FirstOrDefault();
            if (currentPharmacy != null)
            {
                return (currentPharmacy.OwnerId == currentUserId);
            }
            return false;
        }

        public void AddMedicine(Medicine medicine, int currentUserId)
        {
            SetUser(currentUserId);
            CheckPermission((int)Claims.ManageMedicine);
            repository.AddMedicine(medicine);
        }


        public void AddPharmacy(Pharmacy pharmacy, int currentUserId)
        {
            SetUser(currentUserId);
            if (HasClaim((int)Claims.ManagePharmacy))
            {
                repository.AddPharmacy(pharmacy);
                return;
            }
            CheckPermission((int)Claims.ManagePharmacyOwner);
            pharmacy.OwnerId = currentUserId;
            repository.AddPharmacy(pharmacy);
        }

        public void DeleteMedicine(Medicine medicine, int currentUserId)
        {
            SetUser(currentUserId);
            CheckPermission((int)Claims.ManageMedicine);
            repository.DeleteMedicine(medicine);
        }

       
        public void DeletePharmacy(Pharmacy pharmacy, int currentUserId)
        {
            SetUser(currentUserId);
            if (HasClaim((int)Claims.ManagePharmacy))
            {
                repository.DeletePharmacy(pharmacy);
                return;
            }
            CheckPermission((int)Claims.ManagePharmacyOwner);
            if (isOwner(pharmacy.Id, currentUserId))
            {
                repository.DeletePharmacy(pharmacy);
                return;
            }
            throw new Exception("You do not have rights to perform this action");
        }

        public List<Medicine> GetAllMedicine(int currentUserID)
        {
            
            return repository.GetAllMedicine();
        }

        public List<PharmacyMedicine> GetAllMedicineForPharmacy(int pharmacyId, int currentUserId)
        {
            return repository.GetAllMedicineForPharmacy(pharmacyId);
        }

        public List<Pharmacy> GetAllPharmacies(int currentUserID)
        {
            return repository.GetAllPharmacies();
        }

        public void UpdateMedicine(Medicine medicine, int currentUserId)
        {
            SetUser(currentUserId);
            CheckPermission((int)Claims.ManageMedicine);
            repository.UpdateMedicine(medicine);
        }



        private void updateMedicineInPharmacy(PharmacyMedicine medicine)
        {
                repository.UpdateMedicineInPharmacy(medicine);
        }
        public void UpdateMedicineInPharmacy(PharmacyMedicine medicine, int currentUserId)
        {
            SetUser(currentUserId);
            if (HasClaim((int)Claims.ManageMedicineInPharmacies))
            {
                updateMedicineInPharmacy(medicine);
                return;
            }
            CheckPermission((int)Claims.ManageMedicineInPharmaciesOwner);
            if (isOwner(medicine.IdPharmacy, currentUserId))
            {
                updateMedicineInPharmacy(medicine);
                return;
            }
            throw new Exception("You do not have rights to perform this action");
        }

        public void UpdatePharmacy(Pharmacy pharmacy, int currentUserId)
        {
            SetUser(currentUserId);
            if (HasClaim((int)Claims.ManagePharmacy))
            {
                repository.UpdatePharmacy(pharmacy);
                return;
            }
            CheckPermission((int)Claims.ManagePharmacyOwner);
            if (isOwner(pharmacy.Id, currentUserId))
            {
                repository.UpdatePharmacy(pharmacy);
                return;
            }
            throw new Exception("You do not have rights to perform this action");
        }
    }
}
