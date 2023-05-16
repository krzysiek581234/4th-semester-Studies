import org.example.Entity;
import org.example.Repository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

class RepositoryTest {
    private Repository repository;

    @BeforeEach
    void setUp() {
        repository = new Repository();
    }

    @Test
    void find_existingEntity_returnsOptionalWithEntity() {
        // Arrange
        Entity entity = new Entity(11,"Name");
        repository.save(entity);

        // Act
        Optional<Entity> result = repository.find("Name");

        // Assert
        assertTrue(result.isPresent());
        assertEquals(entity, result.get());
    }

    @Test
    void find_nonExistingEntity_returnsEmptyOptional() {
        // Arrange
        Entity entity = new Entity(11,"Name");
        repository.save(entity);

        // Act
        Optional<Entity> result = repository.find("NonExistingName");

        // Assert
        assertFalse(result.isPresent());
    }

    @Test
    void delete_existingEntity_removesEntity() {
        // Arrange
        Entity entity = new Entity(10,"Name");
        repository.save(entity);

        // Act
        repository.delete("Name");
        Optional<Entity> result = repository.find("Name");

        // Assert
        assertFalse(result.isPresent());
    }

    @Test
    void delete_nonExistingEntity_throwsIllegalArgumentException() {
        // Assert
        assertThrows(IllegalArgumentException.class, () -> repository.delete("NonExistingName"));
    }

    @Test
    void save_newEntity_savesEntitySuccessfully() {
        // Arrange
        Entity entity = new Entity(12,"Name");

        // Act
        repository.save(entity);
        Optional<Entity> result = repository.find("Name");

        // Assert
        assertTrue(result.isPresent());
        assertEquals(entity, result.get());
    }

    @Test
    void save_entityWithExistingId_throwsIllegalArgumentException() {
        // Arrange
        Entity entity1 = new Entity(13,"Name");
        Entity entity2 = new Entity(13,"Name");
        repository.save(entity1);

        // Assert
        assertThrows(IllegalArgumentException.class, () -> repository.save(entity2));
    }
}
