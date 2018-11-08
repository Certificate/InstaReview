FROM node:8.12.0-jessie

WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
RUN npm install -g nodemon

COPY . .
EXPOSE 80

CMD ["npm", "run", "start-dev"]