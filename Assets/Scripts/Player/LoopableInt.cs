public class LoopableInt
{
    public LoopableInt (int size)
    {
        m_size = size;
        m_value = 0;
    }

    public void Incremement ()
    {
        m_value++;

        //Loop back
        if (m_value > m_size)
        {
            m_value = 0;
        }
    }

    private int m_size;
    private int m_value = 0;

    public int Value
    {
        get
        {
            return m_value;
        }
    }
}
