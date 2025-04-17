# Etapa 1 - build
FROM node:18-alpine AS build
WORKDIR /app
COPY . .
RUN npm install && npm run build

# Etapa 2 - serve com nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
