import org.example.EntityControler;
import org.example.Repository;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.Mockito;

import static org.junit.jupiter.api.Assertions.assertEquals;


public class EntityControlerTest
{
    @Mock
    private Repository repository = Mockito.mock(Repository.class);
    private EntityControler controler = Mockito.mock(EntityControler.class);


    @Test
    public void goodsave()
    {
        String result = controler.save("name", "10");
        assertEquals(result, "done");
    }
    @Test
    public void badsave()
    {
        String result = controler.save("name", "10");
        String result2 = controler.save("name", "10");
        assertEquals(result2, "bad request");
    }
    @Test
    public void deletedonegood()
    {
        String result = controler.save("name", "10");
        String result2 = controler.delete("name");
        assertEquals(result2, "done");
    }
    @Test
    public void deletedonebad()
    {
        String result = controler.save("name", "10");
        String result2 = controler.delete("name2");
        assertEquals(result2, "not found");
    }
    @Test
    public void findgood()
    {
        String result = controler.save("name", "10");
        String result2 = controler.find("name");
        assertEquals(result2, "name");
    }
    @Test
    public void findbad()
    {
        String result = controler.save("name", "10");
        String result2 = controler.delete("name2");
        assertEquals(result2, "not found");
    }

}
