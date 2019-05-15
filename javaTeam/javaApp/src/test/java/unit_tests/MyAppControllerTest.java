package unit_tests;

import org.junit.*;

import myapp.MyAppController;

import static org.junit.Assert.assertEquals;

public class MyAppControllerTest {
    
    @Test
    public void testIndex() {
        MyAppController target = new MyAppController();
        String results = target.index();

        assertEquals(results, "index.html");
    }
}