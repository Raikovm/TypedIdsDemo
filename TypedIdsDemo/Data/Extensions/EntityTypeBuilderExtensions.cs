namespace TypedIdsDemo.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasTypedIdProperty<TEntity, TProperty>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TProperty>> propertyExpression) 
        where TEntity : class
        where TProperty : ITypedId, new()  =>
        builder.Property(propertyExpression)
            .HasConversion(id => id.Value, value => new TProperty
            {
                Value = value
            });

    public static KeyBuilder HasTypedIdPrimaryKey<TEntity, TProperty>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TProperty>> keyExpression)
        where TEntity : class 
        where TProperty : ITypedId, new()
    {
        builder.HasTypedIdProperty(keyExpression)
            .ValueGeneratedOnAdd();
        
        return builder.HasKey(ConvertExpression(keyExpression));
    }
    
    private static Expression<Func<TEntity, object?>> ConvertExpression<TEntity, TProperty>(
        Expression<Func<TEntity, TProperty>> expression)
    {
        Expression body = Expression.Convert(expression.Body, typeof(object));
        return  Expression.Lambda<Func<TEntity, object?>>(body, expression.Parameters[0]);
    }
}