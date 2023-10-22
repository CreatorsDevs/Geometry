# Cube Dash
(Development name: Geometry)

An engaging game experience that keeps players entertained.

## Design Patterns Used

Throughout the development of this game, several design patterns were employed to demonstrate and apply principles of good software design. The design patterns used include:

1. **Singleton Pattern:** This pattern ensures a class has only one instance and provides a global point of access. In the game, this was useful for managing unique instances such as the `GameManager`, `AudioManager`, and other service-related classes.

2. **Service Locator Pattern:** Implemented to provide a global point of access so objects can easily get references to the services they need.

3. **Object Pooling Pattern:** Introduced to enhance performance by "recycling" game objects rather than continuously creating and destroying them. This was especially beneficial for frequently spawned objects like hurdles.

4. **MVC-S (Model-View-Controller-Service) for Player:** This architectural pattern separates the representation of information from user interaction. The model represents data and business rules, the view handles UI and presentation, the controller manages user input, and the service acts as a bridge facilitating communication between the model and controller. In the game, this pattern ensured a clear distinction between the Player's data, visuals, and interactions, leading to a modular and organized codebase.

5. **State Pattern:** Applied to manage different states of the player which are boost and normal state.

6. **Observer Pattern:** Implemented to create a subscription mechanism where objects can notify multiple other objects about changes. This helped in sending game state updates, such as activating the instructions panel as game starts.

## The Double-Edged Sword of Design Patterns in Small-Scale Games

Design patterns can be incredibly useful tools for creating structured, maintainable, and scalable software. However, it's essential to understand the context in which you're working.

### Pros of Using Design Patterns:
- **Structured Code:** Design patterns can help in organizing the codebase, making it more readable and maintainable.
- **Scalability:** If you plan to expand your game in the future, having a foundation built on design patterns can make the process smoother.
- **Reusability:** Patterns like Object Pooling can be reused across different games or projects, speeding up development.

### Cons:
- **Overhead:** Introducing design patterns in a small-scale game can add unnecessary complexity, making the codebase harder to grasp for newcomers or even the original developers after some time.
- **Performance Costs:** Some patterns might introduce slight performance overheads, which, in the context of a smaller game, might outweigh the benefits.
- **Overengineering:** Not every game needs the rigorous structure provided by design patterns. It's essential to find a balance and not implement a pattern just for the sake of it.

## Conclusion

While design patterns provide proven solutions to common problems, it's crucial to assess the needs and scale of your project before implementing them. In small-scale games, the key is to find a balance between maintainability, performance, and simplicity. In this project, I implemented multiple design patterns to demonstrate the aforementioned points regarding the drawbacks of using them in a small-scale project.

## License

MIT License
