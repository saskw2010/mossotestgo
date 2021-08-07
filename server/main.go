package main

import (
	"log"
	"net/http"

	"github.com/go-chi/chi"
	"github.com/go-chi/cors"
	"github.com/go-chi/chi/middleware"
	"github.com/go-chi/render"

	// CONFIG
	"skaffolder/mossotest/config"
	"skaffolder/mossotest/security"

	// start import model

	// APIs
	"skaffolder/mossotest/api/mossotest_db/user"
	"skaffolder/mossotest/api/mossotest_db/category"
	"skaffolder/mossotest/api/mossotest_db/product"

	// end import model

)

func Routes(configuration *config.Config) *chi.Mux {
	router := chi.NewRouter()
	
	// Basic CORS
	cors := cors.New(cors.Options{
		AllowedOrigins:   []string{"*"},
		AllowedMethods:   []string{"GET", "POST", "PUT", "DELETE", "OPTIONS"},
		AllowedHeaders:   []string{"Accept", "Authorization", "Content-Type", "X-CSRF-Token"},
		AllowCredentials: true,
		MaxAge:           300,
	})

	router.Use(
		cors.Handler,
		render.SetContentType(render.ContentTypeJSON), // Set content-Type headers as application/json
		middleware.Logger,          // Log API request calls
		middleware.DefaultCompress, // Compress results, mostly gzipping assets and json
		middleware.RedirectSlashes, // Redirect slashes to no slash URL versions
		middleware.Recoverer,       // Recover from panics without crashing server
	)

	// Routing
	router.Route("/api", func(r chi.Router) { 
		r.Mount("/", security.New(configuration).Routes())
		r.Mount("/user", user.New(configuration).Routes())
		r.Mount("/category", category.New(configuration).Routes())
		r.Mount("/product", product.New(configuration).Routes())
	})

	return router
}

func main() {
	configuration, err := config.New()
	if err != nil {
		log.Panicln("Configuration error", err)
	}
	router := Routes(configuration)

	walkFunc := func(method string, route string, handler http.Handler, middlewares ...func(http.Handler) http.Handler) error {
		log.Printf("%s %s\n", method, route) // Walk and print out all routes
		return nil
	}
	if err := chi.Walk(router, walkFunc); err != nil {
		log.Panicf("Logging err: %s\n", err.Error()) // panic if there is an error
	}

	// Static filer
	fs := http.FileServer(http.Dir(configuration.Constants.PUBLIC))
	router.Handle("/*", http.StripPrefix("/", fs))

	// Start
	log.Println("Serving application at PORT :" + configuration.Constants.PORT)
	log.Fatal(http.ListenAndServe(":"+configuration.Constants.PORT, router))

}
