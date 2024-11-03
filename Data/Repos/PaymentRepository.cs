using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MSMS.Models.Payments;

namespace MSMS.Data.Repos;

public class PaymentRepository : IPaymentDatabaseRepository
{
    private readonly DatabaseContext context;

    public PaymentRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public IEnumerable<Payment> GetAll()
    {
        return [.. context.Payments.Include(p => p.Patient)];
    }
    public Payment? GetById(int id)
    {
        return context.Payments.Find(id);
    }

    public IEnumerable<Payment?> GetAllPaymentByPaymentStatus(PaymentStatus status)
    {
        return context.Payments.Where(p => p.PaymentStatus == status);
    }

    public IEnumerable<Payment?> GetAllPaymentsByServiceDate(DateOnly serviceDate)
    {
        return context.Payments.Where(p => p.ServiceDate == serviceDate);
    }

    public IEnumerable<Payment?> GetAllPaymentsByPaymentType(PaymentType type)
    {
        return context.Payments.Where(p => p.PaymentType == type);
    }

    public IEnumerable<Payment?> GetAllPaymentsOfPatient(Patient patient)
    {
        return context.Payments.Where(p => p.Patient.Id == patient.Id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Update(Payment model)
    {
        context.Update(model);
    }
}

    
