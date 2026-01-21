# Client

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 20.3.2.

## Features

### User Management

- **User List**: View paginated list of users with search and filter capabilities
- **Search**: Real-time search by name or email with 300ms debouncing
- **Filter by Status**: Filter users by active/inactive status
- **Responsive Design**: Mobile-friendly user interface

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

### Server-Side Rendering (SSR)

To run the application with SSR for production-like environment:

1. Build the project:

```bash
ng build
```

2. Start the SSR server:

```bash
npm run serve:ssr:client
```

**Note**: For development with self-signed SSL certificates, the SSR server is configured to bypass certificate validation via `NODE_TLS_REJECT_UNAUTHORIZED=0`. This should never be used in production.

## API Integration

The application connects to a backend API at `https://localhost:7115/api/Users`.

### Query Parameters

- `search` - Filter users by name or email
- `isActive` - Filter users by active status (true/false)

Example: `https://localhost:7115/api/Users?search=john&isActive=true`

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
