# Engineering Rules

This document summarizes the working standards for this repository.

## Architecture

- Prefer minimal APIs for simple HTTP workflows
- Organize by feature so related files stay together
- Keep `Core` limited to real cross-cutting concerns
- Put business rules in services, not in endpoints
- Avoid unnecessary abstractions for this codebase

## Logging

- Use `ILogger<T>` with structured logging
- Log important business events such as checkout and return actions
- Keep logs useful for debugging without adding noise
- Preserve Serilog as the structured logging pipeline

## Validation

- Use FluentValidation for request validation
- Keep validation close to the request model
- Return clear client-facing validation messages

## Error Handling

- Return clear JSON error responses
- Use appropriate HTTP status codes
- Prefer simple, readable error handling over custom framework layers

## Security

- Keep security headers centralized
- Keep reviewer experience in mind for local Swagger usage
- Do not remove security measures casually; make deliberate exceptions explicit
- Production security defaults should be stricter than this take-home setup

## Code Style

- Keep methods small and readable
- Prefer plain C# records for request and response contracts
- Use simple mutable classes for domain entities when needed
- Favor early returns over nested conditionals
- Keep endpoint files thin

## Testing

- Business rules should be testable through focused services
- Endpoint flows should be covered by integration tests when added
- Prioritize coverage of core flows over exhaustive edge-case infrastructure tests

## Build Quality

- Keep the solution building cleanly
- Treat warnings seriously
- Avoid partial or broken work on `main`
- Preserve a reviewer-friendly local setup

## Git Workflow

- Follow the repository trunk-based workflow
- Keep changes small and focused
- Prefer clear commit messages
- Keep `main` releasable and buildable
