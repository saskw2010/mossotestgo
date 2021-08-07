package category

import (
	"skaffolder/mossotest/config"
	"skaffolder/mossotest/security"

	"github.com/go-chi/chi"
)

type Config struct {
	*config.Config
}

func New(configuration *config.Config) *Config {
	return &Config{configuration}
}

// Routes
func (config *Config) Routes() *chi.Mux {
	router := chi.NewRouter()

	// start routing

	router.Group(func(router chi.Router) { 
		router.Use(security.HasRole())
		router.Post("/", config.create)
	})

	router.Group(func(router chi.Router) { 
		router.Use(security.HasRole())
		router.Delete("/{id}", config.delete)
	})

	router.Group(func(router chi.Router) { 
		router.Use(security.HasRole())
		router.Get("/{id}", config.get)
	})

	router.Group(func(router chi.Router) { 
		router.Use(security.HasRole())
		router.Get("/", config.list)
	})

	router.Group(func(router chi.Router) { 
		router.Use(security.HasRole())
		router.Post("/{id}", config.update)
	})

	// end routing

	// Write here your custom APIs
	// EXAMPLE :

	/**
	router.Group(func(router chi.Router) {
		router.Get("/", config.listCustom) // Create the listCustom method in this file
	})
	*/

	return router
}
