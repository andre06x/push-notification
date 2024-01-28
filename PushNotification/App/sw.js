
self.addEventListener('install', event => {
    console.log('O Service Worker foi instalado.');
});

self.addEventListener('activate', event => {
    console.log('O Service Worker foi ativado.');
});

self.addEventListener('fetch', event => {
    console.log('Interceptou um fetch para:', event.request.url);
});
