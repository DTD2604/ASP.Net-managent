namespace StudentManager.test;

using Microsoft.EntityFrameworkCore;
using Moq;

public static class DbSetMockingExtensions
{
    public static DbSet<T> ReturnsDbSet<T>(this Mock<DbSet<T>> mockSet, IQueryable<T> data) where T : class
    {
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet.Object;
    }
}