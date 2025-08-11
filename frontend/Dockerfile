FROM node:22 AS build
WORKDIR /app

COPY package*.json .
RUN npm ci

COPY . .
RUN npm run build -- --configuration production --output-path dist

FROM nginx:1.27-alpine
COPY --from=build /app/dist/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8080
ENTRYPOINT ["nginx", "-g", "daemon off;"]
