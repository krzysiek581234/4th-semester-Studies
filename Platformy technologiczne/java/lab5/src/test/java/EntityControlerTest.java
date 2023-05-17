import org.example.Entity;
import org.example.EntityControler;
import org.example.Repository;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.Mockito;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.*;


public class EntityControlerTest
{
    @Mock
    private Repository repository = Mockito.mock(Repository.class);
    private EntityControler controler = new EntityControler(repository);
    @Test
    public void findgoodEmpty()
    {
        when(repository.find("test1")).thenReturn(Optional.empty());
        assertEquals(controler.find("test1"), "notfound");
    }
    @Test
    public void findgood()
    {
        when(repository.find("test2")).thenReturn(Optional.of(new Entity(1, "test2")));
        assertEquals(controler.find("test2"), "test2");
    }


    @Test
    public void BadSave() {
        doThrow(new IllegalArgumentException("bad request"))
                .when(repository)
                .save(isA(Entity.class));

        assertEquals("bad request", controler.save("test2", "1"));
    }
    @Test
    public void GoodSave()
    {
        doNothing().when(repository).save(new Entity(1, "test2"));
        assertEquals("done", controler.save("test2", "1"));
    }

    @Test
    public void Baddelete()
    {
        doThrow(new IllegalArgumentException("Object with the specified name does not exist"))
                .when(repository)
                .delete("test2");

        assertEquals("not found", controler.delete("test2"));
    }
    @Test
    public void Gooddone()
    {
        doNothing().when(repository).delete("test2");
        assertEquals("done", controler.delete("test2"));
    }


}
