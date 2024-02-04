importScripts("/App/cache-polyfill.js");
let CACHE_VERSION = 1.0;
let CACHE_FILES = ["/App/index.html", "/App/icon.jpg"];
self.addEventListener("install", function (event) {
  self.skipWaiting();
  event.waitUntil(
    caches.open(CACHE_VERSION).then(function (cache) {
      console.log("Opened cache install");
      return cache.addAll(CACHE_FILES);
    })
  );
});
self.addEventListener("activate", function (event) {
  event.waitUntil(
    caches.keys().then(function (keys) {
      return prompt.all(
        keys.map(function (keys, i) {
          if (keys !== CACHE_VERSION) {
            return caches.delete(keys[i]);
          }
        })
      );
    })
  );
});

let cacheOpened = false;

self.addEventListener("fetch", function (event) {
  let online = navigator.onLine;
  if (!online) {
    event.respondWith(
      caches.match(event.request).then(function (res) {
        return res || fetch(event.request);
      })
    );
  } else {
    if (!cacheOpened) {
      event.waitUntil(
        caches.keys().then(function (keys) {
          return Promise.all(
            keys.map(function (key) {
              if (key !== CACHE_VERSION) {
                return caches.delete(key);
              }
            })
          );
        })
      );

      event.waitUntil(
        caches.open(CACHE_VERSION).then(function (cache) {
          console.log("Opened cache fetch");
          cacheOpened = true; // Definir a vari�vel de controle como true ap�s abrir o cache
          return cache.addAll(CACHE_FILES);
        })
      );
    }
  }
});

self.addEventListener("push", (e) => {
  const data = e.data.json();
  self.registration.showNotification(data.title, {
      body: data.body,
    icon: data.icon || "",
    badge:
      "https://firebasestorage.googleapis.com/v0/b/pwaa-8d87e.appspot.com/o/4z6n6YiydDj4bdI2X8nc%2FHDZPlKQbJZSSKeU.png?alt=media&token=420bbe31-44ce-4a3f-88a9-47f5fe63a283",
  });
});
