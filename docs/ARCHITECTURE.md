# Architecture

## Why Minimal APIs

Minimal APIs fit this take-home well because the surface area is small and the endpoints are straightforward. They keep the bootstrap code light, remove controller ceremony, and make the routing easy to scan quickly.

## Why Feature Folders

The code is organized by feature so the request contract, endpoint, and related logic stay close together. That keeps the project easy to navigate and easy to explain without introducing extra layers.

Examples:

- `Features/Books/GetBooks`
- `Features/Members/GetMembers`
- `Features/Checkouts/CheckoutBook`

## Why the Core Folder Is Small

`Core` only contains cross-cutting concerns that are reused across the API:

- logging
- security headers
- error response helpers
- app and service registration extensions

It is intentionally small so feature code stays in feature folders instead of drifting into a generic shared area.

## Why In-Memory Seeded Data

For a 2-hour take-home, in-memory seeded data is the fastest way to deliver a complete working API with realistic flows. It removes database setup, keeps reviewer onboarding simple, and still demonstrates the business rules clearly in Swagger.

The seed data is designed to show:

- available books
- active checkouts
- overdue checkouts
- valid members for testing

## Where Business Rules Live

Business rules live in focused services, not in the endpoints.

- `BooksService` handles book availability projection
- `MembersService` returns member data
- `CheckoutService` handles checkout, return, due date, and overdue rules

Endpoints stay thin and are responsible for:

- request binding
- validation
- translating service results into HTTP responses
- Swagger metadata

## How This Could Evolve in Production

This design can evolve without a rewrite:

- Replace the in-memory store with a database-backed persistence layer
- Introduce integration tests and service-level unit tests
- Enforce authentication and authorization by default
- Expand error handling into a more complete problem-details strategy
- Add operational concerns such as health checks, metrics, tracing, and configuration management

The current shape is intentionally small, but the separation between features, services, and cross-cutting concerns gives it a clean path forward.
