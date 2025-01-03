namespace Root
{
    public interface IHealthAndDamage
    {
        public int GetCurrentHealth();
        
        public void TakeDamage(int damage);
    }
}