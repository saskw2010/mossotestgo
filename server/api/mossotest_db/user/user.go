package user

import (
	"skaffolder/mossotest/config"
	"skaffolder/mossotest/security"

	"github.com/go-chi/chi"

	"encoding/json"
	"errors"
	"net/http"
	"github.com/globalsign/mgo/bson"
	"github.com/go-chi/render"
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
		router.Use(security.HasRole("ADMIN"))
		router.Post("/{id}/changePassword", config.changePasswordCustom)
	})

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

/*
Name: changePassword
Description: Change password of user from admin
Params:
*/
func (config *Config) changePasswordCustom(writer http.ResponseWriter, req *http.Request) {

	// Get body vars
	type PwdUser struct {
		Id            string `json:"id"`
		PasswordAdmin string `json:"passwordAdmin"`
		PasswordNew   string `json:"passwordNew"`
	}
	var body PwdUser
	json.NewDecoder(req.Body).Decode(&body)

	// Check admin password
	var result User
	err := config.Database.C("User").Find(bson.M{"username": "admin", "password": body.PasswordAdmin}).One(&result)
	if err != nil {
		render.Status(req, 403)
		err = errors.New("Old password not valid")
		render.JSON(writer, req, nil)
		return
	}

	// Update password
	config.Database.C("User").UpdateId(bson.ObjectIdHex(body.Id), bson.M{"$set": bson.M{"password": body.PasswordNew}})

	render.JSON(writer, req, nil)
}
